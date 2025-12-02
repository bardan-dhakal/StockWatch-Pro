package com.stockwatch.stockwatchpro.dtos;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.Min;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class UpdateStockRequestDto {
    private String companyName;

    @DecimalMin(value = "0.0", inclusive = false, message = "Purchase price must be greater than 0")
    private BigDecimal purchase;

    @DecimalMin(value = "0.0", message = "Last dividend cannot be negative")
    private BigDecimal lastDiv;

    private String industry;

    @Min(value = 0, message = "Market cap cannot be negative")
    private Long marketCap;
}
