package com.stockwatch.stockwatchpro.repositories;

import com.stockwatch.stockwatchpro.models.Stock;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface StockRepository extends JpaRepository<Stock, Integer> {
    Optional<Stock> findBySymbol(String symbol);
    List<Stock> findByIndustry(String industry);

    @Query("SELECT s FROM Stock s WHERE s.companyName ILIKE %:companyName%")
    List<Stock> searchByCompanyName(@Param("companyName") String companyName);

    boolean existsBySymbol(String symbol);
}
