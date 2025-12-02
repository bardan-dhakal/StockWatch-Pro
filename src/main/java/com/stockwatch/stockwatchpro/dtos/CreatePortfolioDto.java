package com.stockwatch.stockwatchpro.dtos;

import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CreatePortfolioDto {
    @NotNull(message = "Stock ID is required")
    private Integer stockId;
}
