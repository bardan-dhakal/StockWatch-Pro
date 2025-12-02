package com.stockwatch.stockwatchpro.repositories;

import com.stockwatch.stockwatchpro.models.Portfolio;
import com.stockwatch.stockwatchpro.models.PortfolioId;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface PortfolioRepository extends JpaRepository<Portfolio, PortfolioId> {
    @Query("SELECT p FROM Portfolio p WHERE p.appUserId = :userId")
    List<Portfolio> findByAppUserId(@Param("userId") String userId);

    @Query("SELECT p FROM Portfolio p WHERE p.stockId = :stockId")
    List<Portfolio> findByStockId(@Param("stockId") Integer stockId);

    @Query("SELECT CASE WHEN COUNT(p) > 0 THEN true ELSE false END FROM Portfolio p WHERE p.appUserId = :userId AND p.stockId = :stockId")
    boolean existsByAppUserIdAndStockId(@Param("userId") String userId, @Param("stockId") Integer stockId);
}
