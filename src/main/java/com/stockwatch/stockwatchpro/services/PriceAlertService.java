package com.stockwatch.stockwatchpro.services;

import com.stockwatch.stockwatchpro.dtos.CreatePriceAlertDto;
import com.stockwatch.stockwatchpro.dtos.PriceAlertDto;
import com.stockwatch.stockwatchpro.models.PriceAlert;
import com.stockwatch.stockwatchpro.repositories.PriceAlertRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
@Transactional
public class PriceAlertService {
    private final PriceAlertRepository priceAlertRepository;
    private final AppUserService appUserService;
    private final StockService stockService;

    public PriceAlertService(PriceAlertRepository priceAlertRepository, AppUserService appUserService, StockService stockService) {
        this.priceAlertRepository = priceAlertRepository;
        this.appUserService = appUserService;
        this.stockService = stockService;
    }

    public PriceAlertDto createPriceAlert(String userId, CreatePriceAlertDto createPriceAlertDto) {
        appUserService.findById(userId);
        stockService.getStockById(createPriceAlertDto.getStockId());

        PriceAlert.AlertType alertType = PriceAlert.AlertType.valueOf(createPriceAlertDto.getAlertType().toUpperCase());

        PriceAlert priceAlert = PriceAlert.builder()
                .appUserId(userId)
                .stockId(createPriceAlertDto.getStockId())
                .targetPrice(createPriceAlertDto.getTargetPrice())
                .alertType(alertType)
                .isActive(true)
                .createdAt(LocalDateTime.now())
                .build();

        PriceAlert savedAlert = priceAlertRepository.save(priceAlert);
        return mapToDto(savedAlert);
    }

    public PriceAlertDto getPriceAlertById(Integer id) {
        PriceAlert priceAlert = priceAlertRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Price alert not found with id: " + id));
        return mapToDto(priceAlert);
    }

    public List<PriceAlertDto> getUserActiveAlerts(String userId) {
        appUserService.findById(userId);
        return priceAlertRepository.findActiveAlertsByUserId(userId).stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public List<PriceAlertDto> getUserAllAlerts(String userId) {
        appUserService.findById(userId);
        return priceAlertRepository.findByAppUserId(userId).stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public List<PriceAlertDto> getStockActiveAlerts(Integer stockId) {
        stockService.getStockById(stockId);
        return priceAlertRepository.findActiveAlertsByStockId(stockId).stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public PriceAlertDto togglePriceAlert(Integer id, Boolean isActive) {
        PriceAlert priceAlert = priceAlertRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Price alert not found with id: " + id));

        priceAlert.setIsActive(isActive);
        if (!isActive) {
            priceAlert.setTriggeredAt(null);
        }

        PriceAlert updatedAlert = priceAlertRepository.save(priceAlert);
        return mapToDto(updatedAlert);
    }

    public void deletePriceAlert(Integer id) {
        PriceAlert priceAlert = priceAlertRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Price alert not found with id: " + id));
        priceAlertRepository.delete(priceAlert);
    }

    private PriceAlertDto mapToDto(PriceAlert priceAlert) {
        return PriceAlertDto.builder()
                .id(priceAlert.getId())
                .appUserId(priceAlert.getAppUserId())
                .stockId(priceAlert.getStockId())
                .targetPrice(priceAlert.getTargetPrice())
                .alertType(priceAlert.getAlertType().name())
                .isActive(priceAlert.getIsActive())
                .createdAt(priceAlert.getCreatedAt())
                .triggeredAt(priceAlert.getTriggeredAt())
                .build();
    }
}
