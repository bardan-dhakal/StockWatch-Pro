package com.stockwatch.stockwatchpro.services;

import com.stockwatch.stockwatchpro.dtos.CreateStockRequestDto;
import com.stockwatch.stockwatchpro.dtos.StockDto;
import com.stockwatch.stockwatchpro.dtos.UpdateStockRequestDto;
import com.stockwatch.stockwatchpro.models.Stock;
import com.stockwatch.stockwatchpro.repositories.StockRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.stream.Collectors;

@Service
@Transactional
public class StockService {
    private final StockRepository stockRepository;

    public StockService(StockRepository stockRepository) {
        this.stockRepository = stockRepository;
    }

    public StockDto createStock(CreateStockRequestDto createStockRequestDto) {
        if (stockRepository.existsBySymbol(createStockRequestDto.getSymbol())) {
            throw new IllegalArgumentException("Stock with symbol " + createStockRequestDto.getSymbol() + " already exists");
        }

        Stock stock = Stock.builder()
                .symbol(createStockRequestDto.getSymbol())
                .companyName(createStockRequestDto.getCompanyName())
                .purchase(createStockRequestDto.getPurchase())
                .lastDiv(createStockRequestDto.getLastDiv())
                .industry(createStockRequestDto.getIndustry())
                .marketCap(createStockRequestDto.getMarketCap())
                .build();

        Stock savedStock = stockRepository.save(stock);
        return mapToDto(savedStock);
    }

    public StockDto getStockById(Integer id) {
        Stock stock = stockRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Stock not found with id: " + id));
        return mapToDto(stock);
    }

    public StockDto getStockBySymbol(String symbol) {
        Stock stock = stockRepository.findBySymbol(symbol)
                .orElseThrow(() -> new IllegalArgumentException("Stock not found with symbol: " + symbol));
        return mapToDto(stock);
    }

    public List<StockDto> getAllStocks() {
        return stockRepository.findAll().stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public List<StockDto> getStocksByIndustry(String industry) {
        return stockRepository.findByIndustry(industry).stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public List<StockDto> searchByCompanyName(String companyName) {
        return stockRepository.searchByCompanyName(companyName).stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public StockDto updateStock(Integer id, UpdateStockRequestDto updateStockRequestDto) {
        Stock stock = stockRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Stock not found with id: " + id));

        if (updateStockRequestDto.getCompanyName() != null) {
            stock.setCompanyName(updateStockRequestDto.getCompanyName());
        }
        if (updateStockRequestDto.getPurchase() != null) {
            stock.setPurchase(updateStockRequestDto.getPurchase());
        }
        if (updateStockRequestDto.getLastDiv() != null) {
            stock.setLastDiv(updateStockRequestDto.getLastDiv());
        }
        if (updateStockRequestDto.getIndustry() != null) {
            stock.setIndustry(updateStockRequestDto.getIndustry());
        }
        if (updateStockRequestDto.getMarketCap() != null) {
            stock.setMarketCap(updateStockRequestDto.getMarketCap());
        }

        Stock updatedStock = stockRepository.save(stock);
        return mapToDto(updatedStock);
    }

    public void deleteStock(Integer id) {
        Stock stock = stockRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Stock not found with id: " + id));
        stockRepository.delete(stock);
    }

    private StockDto mapToDto(Stock stock) {
        return StockDto.builder()
                .id(stock.getId())
                .symbol(stock.getSymbol())
                .companyName(stock.getCompanyName())
                .purchase(stock.getPurchase())
                .lastDiv(stock.getLastDiv())
                .industry(stock.getIndustry())
                .marketCap(stock.getMarketCap())
                .build();
    }
}
