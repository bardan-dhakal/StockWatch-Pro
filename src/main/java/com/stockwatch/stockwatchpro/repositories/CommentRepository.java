package com.stockwatch.stockwatchpro.repositories;

import com.stockwatch.stockwatchpro.models.Comment;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface CommentRepository extends JpaRepository<Comment, Integer> {
    @Query("SELECT c FROM Comment c WHERE c.stockId = :stockId ORDER BY c.createdTime DESC")
    List<Comment> findByStockIdOrderByCreatedTimeDesc(@Param("stockId") Integer stockId);

    @Query("SELECT COUNT(c) FROM Comment c WHERE c.stockId = :stockId")
    long countByStockId(@Param("stockId") Integer stockId);
}
