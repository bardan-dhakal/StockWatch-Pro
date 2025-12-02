package com.stockwatch.stockwatchpro.dtos;

import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CreateStockRequestDto {
    @NotBlank(message = "Symbol is required")
    @Size(min = 1, max = 10, message = "Symbol must be between 1 and 10 characters")
    private String symbol;

    @NotBlank(message = "Company name is required")
    private String companyName;

    @NotNull(message = "Purchase price is required")
    @DecimalMin(value = "0.0", inclusive = false, message = "Purchase price must be greater than 0")
    private BigDecimal purchase;

    @NotNull(message = "Last dividend is required")
    @DecimalMin(value = "0.0", message = "Last dividend cannot be negative")
    private BigDecimal lastDiv;

    private String industry;

    @Min(value = 0, message = "Market cap cannot be negative")
    private Long marketCap;
}
