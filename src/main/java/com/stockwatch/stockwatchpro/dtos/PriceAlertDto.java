package com.stockwatch.stockwatchpro.dtos;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;
import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class PriceAlertDto {
    private Integer id;
    private String appUserId;
    private Integer stockId;
    private BigDecimal targetPrice;
    private String alertType;
    private Boolean isActive;
    private LocalDateTime createdAt;
    private LocalDateTime triggeredAt;
}
