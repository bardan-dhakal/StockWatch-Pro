package com.stockwatch.stockwatchpro.controllers;

import com.stockwatch.stockwatchpro.dtos.CommentDto;
import com.stockwatch.stockwatchpro.services.CommentService;
import jakarta.validation.constraints.NotBlank;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/comment")
@CrossOrigin(origins = "*")
public class CommentController {
    private final CommentService commentService;

    public CommentController(CommentService commentService) {
        this.commentService = commentService;
    }

    @PostMapping
    public ResponseEntity<CommentDto> createComment(
            @RequestParam Integer stockId,
            @RequestParam @NotBlank String title,
            @RequestParam @NotBlank String content) {
        try {
            CommentDto comment = commentService.createComment(stockId, title, content);
            return ResponseEntity.status(HttpStatus.CREATED).body(comment);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().build();
        }
    }

    @GetMapping("/{id}")
    public ResponseEntity<CommentDto> getCommentById(@PathVariable Integer id) {
        try {
            CommentDto comment = commentService.getCommentById(id);
            return ResponseEntity.ok(comment);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @GetMapping("/stock/{stockId}")
    public ResponseEntity<List<CommentDto>> getCommentsByStockId(@PathVariable Integer stockId) {
        try {
            List<CommentDto> comments = commentService.getCommentsByStockId(stockId);
            return ResponseEntity.ok(comments);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @PutMapping("/{id}")
    public ResponseEntity<CommentDto> updateComment(
            @PathVariable Integer id,
            @RequestParam(required = false) String title,
            @RequestParam(required = false) String content) {
        try {
            CommentDto comment = commentService.updateComment(id, title, content);
            return ResponseEntity.ok(comment);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteComment(@PathVariable Integer id) {
        try {
            commentService.deleteComment(id);
            return ResponseEntity.noContent().build();
        } catch (IllegalArgumentException e) {
            return ResponseEntity.notFound().build();
        }
    }
}
