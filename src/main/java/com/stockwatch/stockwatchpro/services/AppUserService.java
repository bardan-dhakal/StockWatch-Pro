package com.stockwatch.stockwatchpro.services;

import com.stockwatch.stockwatchpro.dtos.RegisterDto;
import com.stockwatch.stockwatchpro.models.AppUser;
import com.stockwatch.stockwatchpro.repositories.AppUserRepository;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
@Transactional
public class AppUserService {
    private final AppUserRepository appUserRepository;
    private final PasswordEncoder passwordEncoder;

    public AppUserService(AppUserRepository appUserRepository, PasswordEncoder passwordEncoder) {
        this.appUserRepository = appUserRepository;
        this.passwordEncoder = passwordEncoder;
    }

    public AppUser registerUser(RegisterDto registerDto) {
        if (appUserRepository.existsByEmail(registerDto.getEmail())) {
            throw new IllegalArgumentException("Email already in use");
        }

        if (appUserRepository.existsByUsername(registerDto.getUsername())) {
            throw new IllegalArgumentException("Username already in use");
        }

        AppUser appUser = AppUser.builder()
                .username(registerDto.getUsername())
                .email(registerDto.getEmail())
                .password(passwordEncoder.encode(registerDto.getPassword()))
                .roles(new ArrayList<>(List.of("ROLE_USER")))
                .build();

        return appUserRepository.save(appUser);
    }

    public Optional<AppUser> findByUsername(String username) {
        return appUserRepository.findByUsername(username);
    }

    public Optional<AppUser> findByEmail(String email) {
        return appUserRepository.findByEmail(email);
    }

    public AppUser findById(String id) {
        return appUserRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("User not found"));
    }

    public boolean validatePassword(String rawPassword, String encodedPassword) {
        return passwordEncoder.matches(rawPassword, encodedPassword);
    }
}
