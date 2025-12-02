package com.stockwatch.stockwatchpro.services;

import com.stockwatch.stockwatchpro.models.AppUser;
import com.stockwatch.stockwatchpro.models.Portfolio;
import com.stockwatch.stockwatchpro.models.PortfolioId;
import com.stockwatch.stockwatchpro.models.Stock;
import com.stockwatch.stockwatchpro.repositories.PortfolioRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Service
@Transactional
public class PortfolioService {
    private final PortfolioRepository portfolioRepository;
    private final AppUserService appUserService;
    private final StockService stockService;

    public PortfolioService(PortfolioRepository portfolioRepository, AppUserService appUserService, StockService stockService) {
        this.portfolioRepository = portfolioRepository;
        this.appUserService = appUserService;
        this.stockService = stockService;
    }

    public Portfolio addStockToPortfolio(String userId, Integer stockId) {
        if (portfolioRepository.existsByAppUserIdAndStockId(userId, stockId)) {
            throw new IllegalArgumentException("Stock already exists in portfolio");
        }

        AppUser appUser = appUserService.findById(userId);
        stockService.getStockById(stockId);

        Portfolio portfolio = Portfolio.builder()
                .appUserId(userId)
                .stockId(stockId)
                .build();

        return portfolioRepository.save(portfolio);
    }

    public void removeStockFromPortfolio(String userId, Integer stockId) {
        Portfolio portfolio = portfolioRepository.findById(new PortfolioId(userId, stockId))
                .orElseThrow(() -> new IllegalArgumentException("Stock not found in portfolio"));
        portfolioRepository.delete(portfolio);
    }

    public List<Portfolio> getUserPortfolio(String userId) {
        appUserService.findById(userId);
        return portfolioRepository.findByAppUserId(userId);
    }

    public List<Portfolio> getStockPortfolios(Integer stockId) {
        return portfolioRepository.findByStockId(stockId);
    }

    public boolean isStockInPortfolio(String userId, Integer stockId) {
        return portfolioRepository.existsByAppUserIdAndStockId(userId, stockId);
    }
}
