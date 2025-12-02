package com.stockwatch.stockwatchpro.services;

import com.stockwatch.stockwatchpro.dtos.CredentialTokenDto;
import com.stockwatch.stockwatchpro.models.AppUser;
import com.stockwatch.stockwatchpro.security.JwtUtil;
import org.springframework.stereotype.Service;

@Service
public class TokenService {
    private final JwtUtil jwtUtil;

    public TokenService(JwtUtil jwtUtil) {
        this.jwtUtil = jwtUtil;
    }

    public CredentialTokenDto createToken(AppUser appUser) {
        String token = jwtUtil.generateToken(appUser.getUsername(), appUser.getEmail());

        return CredentialTokenDto.builder()
                .username(appUser.getUsername())
                .email(appUser.getEmail())
                .token(token)
                .build();
    }
}
