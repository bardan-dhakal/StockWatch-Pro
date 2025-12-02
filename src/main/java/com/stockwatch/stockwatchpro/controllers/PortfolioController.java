package com.stockwatch.stockwatchpro.controllers;

import com.stockwatch.stockwatchpro.dtos.CreatePortfolioDto;
import com.stockwatch.stockwatchpro.models.Portfolio;
import com.stockwatch.stockwatchpro.services.PortfolioService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/portfolio")
@CrossOrigin(origins = "*")
public class PortfolioController {
    private final PortfolioService portfolioService;

    public PortfolioController(PortfolioService portfolioService) {
        this.portfolioService = portfolioService;
    }

    private String getCurrentUserId() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        return authentication != null ? authentication.getName() : null;
    }

    @PostMapping
    public ResponseEntity<Portfolio> addStockToPortfolio(@Valid @RequestBody CreatePortfolioDto createPortfolioDto) {
        try {
            String userId = getCurrentUserId();
            Portfolio portfolio = portfolioService.addStockToPortfolio(userId, createPortfolioDto.getStockId());
            return ResponseEntity.status(HttpStatus.CREATED).body(portfolio);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().build();
        }
    }

    @GetMapping
    public ResponseEntity<List<Portfolio>> getUserPortfolio() {
        try {
            String userId = getCurrentUserId();
            List<Portfolio> portfolio = portfolioService.getUserPortfolio(userId);
            return ResponseEntity.ok(portfolio);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("/{stockId}")
    public ResponseEntity<Void> removeStockFromPortfolio(@PathVariable Integer stockId) {
        try {
            String userId = getCurrentUserId();
            portfolioService.removeStockFromPortfolio(userId, stockId);
            return ResponseEntity.noContent().build();
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/stock/{stockId}")
    public ResponseEntity<List<Portfolio>> getStockPortfolios(@PathVariable Integer stockId) {
        List<Portfolio> portfolios = portfolioService.getStockPortfolios(stockId);
        return ResponseEntity.ok(portfolios);
    }

    @GetMapping("/check/{stockId}")
    public ResponseEntity<Boolean> isStockInPortfolio(@PathVariable Integer stockId) {
        String userId = getCurrentUserId();
        boolean exists = portfolioService.isStockInPortfolio(userId, stockId);
        return ResponseEntity.ok(exists);
    }
}
