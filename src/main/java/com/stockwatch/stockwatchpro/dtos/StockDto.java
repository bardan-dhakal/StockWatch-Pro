package com.stockwatch.stockwatchpro.dtos;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class StockDto {
    private Integer id;
    private String symbol;
    private String companyName;
    private BigDecimal purchase;
    private BigDecimal lastDiv;
    private String industry;
    private Long marketCap;
}
