package com.example.Package.exceptions;

public class DuplicatedNameException extends IllegalArgumentException {
    public DuplicatedNameException(String message) {
        super(message);
    }
}
