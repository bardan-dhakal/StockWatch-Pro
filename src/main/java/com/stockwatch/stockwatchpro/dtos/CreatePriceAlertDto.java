package com.stockwatch.stockwatchpro.dtos;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CreatePriceAlertDto {
    @NotNull(message = "Stock ID is required")
    private Integer stockId;

    @NotNull(message = "Target price is required")
    @DecimalMin(value = "0.0", inclusive = false, message = "Target price must be greater than 0")
    private BigDecimal targetPrice;

    @NotNull(message = "Alert type is required")
    private String alertType;
}
