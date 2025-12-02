package com.stockwatch.stockwatchpro.dtos;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class CredentialTokenDto {
    private String username;
    private String email;
    private String token;
}
