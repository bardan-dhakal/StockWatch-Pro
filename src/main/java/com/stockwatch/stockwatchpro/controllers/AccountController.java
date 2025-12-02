package com.stockwatch.stockwatchpro.controllers;

import com.stockwatch.stockwatchpro.dtos.LoginDto;
import com.stockwatch.stockwatchpro.dtos.RegisterDto;
import com.stockwatch.stockwatchpro.dtos.CredentialTokenDto;
import com.stockwatch.stockwatchpro.models.AppUser;
import com.stockwatch.stockwatchpro.services.AppUserService;
import com.stockwatch.stockwatchpro.services.TokenService;
import jakarta.validation.Valid;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/account")
@CrossOrigin(origins = "*")
public class AccountController {
    private final AppUserService appUserService;
    private final TokenService tokenService;

    public AccountController(AppUserService appUserService, TokenService tokenService) {
        this.appUserService = appUserService;
        this.tokenService = tokenService;
    }

    @PostMapping("/register")
    public ResponseEntity<CredentialTokenDto> register(@Valid @RequestBody RegisterDto registerDto) {
        try {
            AppUser appUser = appUserService.registerUser(registerDto);
            CredentialTokenDto token = tokenService.createToken(appUser);
            return ResponseEntity.status(HttpStatus.CREATED).body(token);
        } catch (IllegalArgumentException e) {
            return ResponseEntity.badRequest().build();
        }
    }

    @PostMapping("/login")
    public ResponseEntity<CredentialTokenDto> login(@Valid @RequestBody LoginDto loginDto) {
        AppUser appUser = appUserService.findByEmail(loginDto.getEmail())
                .orElseThrow(() -> new IllegalArgumentException("Invalid email"));

        if (!appUserService.validatePassword(loginDto.getPassword(), appUser.getPassword())) {
            throw new IllegalArgumentException("Invalid password");
        }

        CredentialTokenDto token = tokenService.createToken(appUser);
        return ResponseEntity.ok(token);
    }
}
