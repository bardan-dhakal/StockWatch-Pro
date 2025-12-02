package com.stockwatch.stockwatchpro.repositories;

import com.stockwatch.stockwatchpro.models.PriceAlert;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface PriceAlertRepository extends JpaRepository<PriceAlert, Integer> {
    @Query("SELECT pa FROM PriceAlert pa WHERE pa.appUserId = :userId AND pa.isActive = true")
    List<PriceAlert> findActiveAlertsByUserId(@Param("userId") String userId);

    @Query("SELECT pa FROM PriceAlert pa WHERE pa.stockId = :stockId AND pa.isActive = true")
    List<PriceAlert> findActiveAlertsByStockId(@Param("stockId") Integer stockId);

    @Query("SELECT pa FROM PriceAlert pa WHERE pa.appUserId = :userId")
    List<PriceAlert> findByAppUserId(@Param("userId") String userId);

    @Query("SELECT pa FROM PriceAlert pa WHERE pa.stockId = :stockId")
    List<PriceAlert> findByStockId(@Param("stockId") Integer stockId);
}
