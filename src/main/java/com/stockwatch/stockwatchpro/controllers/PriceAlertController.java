package com.stockwatch.stockwatchpro.controllers;

import com.stockwatch.stockwatchpro.dtos.CreatePriceAlertDto;
import com.stockwatch.stockwatchpro.dtos.PriceAlertDto;
import com.stockwatch.stockwatchpro.services.PriceAlertService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/pricealert")
@CrossOrigin(origins = "*")
public class PriceAlertController {
    private final PriceAlertService priceAlertService;

    public PriceAlertController(PriceAlertService priceAlertService) {
        this.priceAlertService = priceAlertService;
    }

    private String getCurrentUserId() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        return authentication != null ? authentication.getName() : null;
    }

    @PostMapping
    public ResponseEntity<PriceAlertDto> createPriceAlert(@Valid @RequestBody CreatePriceAlertDto createPriceAlertDto) {
        try {
            String userId = getCurrentUserId();
            PriceAlertDto priceAlert = priceAlertService.createPriceAlert(userId, createPriceAlertDto);
            return ResponseEntity.status(HttpStatus.CREATED).body(priceAlert);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().build();
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<PriceAlertDto> getPriceAlertById(@PathVariable Integer id) {
        try {
            PriceAlertDto priceAlert = priceAlertService.getPriceAlertById(id);
            return ResponseEntity.ok(priceAlert);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/user/active")
    public ResponseEntity<List<PriceAlertDto>> getUserActiveAlerts() {
        try {
            String userId = getCurrentUserId();
            List<PriceAlertDto> alerts = priceAlertService.getUserActiveAlerts(userId);
            return ResponseEntity.ok(alerts);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/user/all")
    public ResponseEntity<List<PriceAlertDto>> getUserAllAlerts() {
        try {
            String userId = getCurrentUserId();
            List<PriceAlertDto> alerts = priceAlertService.getUserAllAlerts(userId);
            return ResponseEntity.ok(alerts);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/stock/{stockId}/active")
    public ResponseEntity<List<PriceAlertDto>> getStockActiveAlerts(@PathVariable Integer stockId) {
        try {
            List<PriceAlertDto> alerts = priceAlertService.getStockActiveAlerts(stockId);
            return ResponseEntity.ok(alerts);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @PatchMapping("/{id}")
    public ResponseEntity<PriceAlertDto> togglePriceAlert(@PathVariable Integer id, @RequestParam Boolean isActive) {
        try {
            PriceAlertDto priceAlert = priceAlertService.togglePriceAlert(id, isActive);
            return ResponseEntity.ok(priceAlert);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deletePriceAlert(@PathVariable Integer id) {
        try {
            priceAlertService.deletePriceAlert(id);
            return ResponseEntity.noContent().build();
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }
}
