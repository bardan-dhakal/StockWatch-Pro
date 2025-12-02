package com.stockwatch.stockwatchpro.services;

import com.stockwatch.stockwatchpro.dtos.CommentDto;
import com.stockwatch.stockwatchpro.models.Comment;
import com.stockwatch.stockwatchpro.repositories.CommentRepository;
import com.stockwatch.stockwatchpro.repositories.StockRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
@Transactional
public class CommentService {
    private final CommentRepository commentRepository;
    private final StockRepository stockRepository;

    public CommentService(CommentRepository commentRepository, StockRepository stockRepository) {
        this.commentRepository = commentRepository;
        this.stockRepository = stockRepository;
    }

    public CommentDto createComment(Integer stockId, String title, String content) {
        stockRepository.findById(stockId)
                .orElseThrow(() -> new IllegalArgumentException("Stock not found with id: " + stockId));

        Comment comment = Comment.builder()
                .stockId(stockId)
                .title(title)
                .content(content)
                .createdTime(LocalDateTime.now())
                .build();

        Comment savedComment = commentRepository.save(comment);
        return mapToDto(savedComment);
    }

    public CommentDto getCommentById(Integer id) {
        Comment comment = commentRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Comment not found with id: " + id));
        return mapToDto(comment);
    }

    public List<CommentDto> getCommentsByStockId(Integer stockId) {
        stockRepository.findById(stockId)
                .orElseThrow(() -> new IllegalArgumentException("Stock not found with id: " + stockId));

        return commentRepository.findByStockIdOrderByCreatedTimeDesc(stockId).stream()
                .map(this::mapToDto)
                .collect(Collectors.toList());
    }

    public CommentDto updateComment(Integer id, String title, String content) {
        Comment comment = commentRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Comment not found with id: " + id));

        if (title != null) {
            comment.setTitle(title);
        }
        if (content != null) {
            comment.setContent(content);
        }

        Comment updatedComment = commentRepository.save(comment);
        return mapToDto(updatedComment);
    }

    public void deleteComment(Integer id) {
        Comment comment = commentRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Comment not found with id: " + id));
        commentRepository.delete(comment);
    }

    private CommentDto mapToDto(Comment comment) {
        return CommentDto.builder()
                .id(comment.getId())
                .title(comment.getTitle())
                .content(comment.getContent())
                .createdTime(comment.getCreatedTime())
                .stockId(comment.getStockId())
                .build();
    }
}
