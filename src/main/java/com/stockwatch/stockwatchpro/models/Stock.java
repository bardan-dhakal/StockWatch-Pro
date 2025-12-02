package com.stockwatch.stockwatchpro.models;

import jakarta.persistence.*;
import lombok.*;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "stocks", indexes = {
        @Index(name = "idx_symbol", columnList = "symbol", unique = true),
        @Index(name = "idx_company_name", columnList = "company_name")
})
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Stock {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer id;

    @Column(nullable = false, unique = true, length = 10)
    private String symbol;

    @Column(name = "company_name", nullable = false)
    private String companyName;

    @Column(nullable = false, precision = 18, scale = 2)
    private BigDecimal purchase;

    @Column(name = "last_div", nullable = false, precision = 18, scale = 2)
    private BigDecimal lastDiv;

    @Column(length = 100)
    private String industry;

    @Column(name = "market_cap")
    private Long marketCap;

    @OneToMany(mappedBy = "stock", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<Comment> comments = new ArrayList<>();

    @OneToMany(mappedBy = "stock", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<Portfolio> portfolios = new ArrayList<>();

    @OneToMany(mappedBy = "stock", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<PriceAlert> priceAlerts = new ArrayList<>();
}
