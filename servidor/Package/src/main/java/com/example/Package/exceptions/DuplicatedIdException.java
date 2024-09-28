package com.example.Package.exceptions;

public class DuplicatedIdException extends IllegalArgumentException {
    public DuplicatedIdException(String message) {
        super(message);
    }
}
