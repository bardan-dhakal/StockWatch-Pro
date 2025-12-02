package com.stockwatch.stockwatchpro.controllers;

import com.stockwatch.stockwatchpro.dtos.CreateStockRequestDto;
import com.stockwatch.stockwatchpro.dtos.StockDto;
import com.stockwatch.stockwatchpro.dtos.UpdateStockRequestDto;
import com.stockwatch.stockwatchpro.services.StockService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/stock")
@CrossOrigin(origins = "*")
public class StockController {
    private final StockService stockService;

    public StockController(StockService stockService) {
        this.stockService = stockService;
    }

    @PostMapping
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<StockDto> createStock(@Valid @RequestBody CreateStockRequestDto createStockRequestDto) {
        try {
            StockDto stock = stockService.createStock(createStockRequestDto);
            return ResponseEntity.status(HttpStatus.CREATED).body(stock);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().build();
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<StockDto> getStockById(@PathVariable Integer id) {
        try {
            StockDto stock = stockService.getStockById(id);
            return ResponseEntity.ok(stock);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/symbol/{symbol}")
    public ResponseEntity<StockDto> getStockBySymbol(@PathVariable String symbol) {
        try {
            StockDto stock = stockService.getStockBySymbol(symbol);
            return ResponseEntity.ok(stock);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping
    public ResponseEntity<List<StockDto>> getAllStocks() {
        List<StockDto> stocks = stockService.getAllStocks();
        return ResponseEntity.ok(stocks);
    }

    @GetMapping("/industry/{industry}")
    public ResponseEntity<List<StockDto>> getStocksByIndustry(@PathVariable String industry) {
        List<StockDto> stocks = stockService.getStocksByIndustry(industry);
        return ResponseEntity.ok(stocks);
    }

    @GetMapping("/search")
    public ResponseEntity<List<StockDto>> searchByCompanyName(@RequestParam String companyName) {
        List<StockDto> stocks = stockService.searchByCompanyName(companyName);
        return ResponseEntity.ok(stocks);
    }

    @PutMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<StockDto> updateStock(@PathVariable Integer id, @Valid @RequestBody UpdateStockRequestDto updateStockRequestDto) {
        try {
            StockDto stock = stockService.updateStock(id, updateStockRequestDto);
            return ResponseEntity.ok(stock);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<Void> deleteStock(@PathVariable Integer id) {
        try {
            stockService.deleteStock(id);
            return ResponseEntity.noContent().build();
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }
}
