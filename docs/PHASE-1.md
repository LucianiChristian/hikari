# Phase 1: Planning & Architecture

## High-Level Goal

**Build a functional flashcard system with CRUD operations and SM-2 spaced repetition study sessions**

## MVP Implementation Steps

1. **CRUD decks** (create, read, update, delete)
2. **CRUD cards in decks** (create, read, update, delete text-based question/answer pairs)
3. **Study sessions with rating functionality** (rate easy/good/hard/again - stored but no scheduling impact)
4. **SM-2 algorithm implementation** (ratings now trigger interval calculations)
5. **Study sessions showing only due cards** (algorithm-driven scheduling)

## Inspirations & Methodology

**Problem Simplification Strategy** inspired by *The Pragmatic Programmer*:
- Started with complex idea (Anki import, migration, full feature parity)
- Applied "divide and conquer" - broke down to core problem: prove spaced repetition algorithm works
- Removed non-essential complexity (file parsing, rich formatting, mobile support)
- Result: Simple CRUD + algorithm implementation that validates core hypothesis

**TDD Approach** inspired by Vladimir Khorikov's *Unit Testing Principles, Practices, and Patterns*:
- Testing at service/handler level rather than HTTP layer
- Focus on behavior verification over implementation details
- Treat tests as living specifications for domain logic
- Avoid over-mocking - test meaningful business operations

## Technical Architecture Decisions

### Data Model Design

**Core Entities:**
- **Deck**: Id, Name, Description, CreatedDate, UserId
- **Card**: Id, DeckId, Front (question), Back (answer), CreatedDate, UpdatedDate  
- **Review**: Id, CardId, UserId, ReviewDate, Rating (1-4), ResponseTimeMs, PreviousInterval, NewInterval
- **CardState**: CardId, UserId, EaseFactor (starts 2.5), CurrentInterval, NextDueDate, ReviewCount, LapseCount

**Key Design Principles:**
- Cards contain content (reusable)
- CardState tracks per-user progress with each card
- Reviews provide complete audit trail for algorithm refinement
- Clean separation between content and user progress

### Project Structure

**Two-Project Architecture:**
- **Hikari.Api**: Controllers, DTOs, HTTP layer
- **Hikari.App**: Services, domain logic, EF Core, repositories, SM-2 algorithm

**Testing Strategy:**
- All business logic testing in **Hikari.App.Tests**
- API layer remains thin HTTP wrapper around tested business logic
- TDD at service level (DeckService, CardService, StudyService)

**Benefits:**
- Simple structure without over-engineering
- Clear separation between HTTP concerns and business logic
- Single target for comprehensive business logic testing

### API Design

**REST Conventions:**
- Standard HTTP verbs (GET, POST, PUT, DELETE)
- Resource-based URLs following RESTful patterns
- JSON request/response payloads

**Authentication:**
- JWT-based authentication via Microsoft Identity
- Bearer token authorization on protected endpoints
- User context extracted from JWT claims for data isolation

**Endpoint Structure (Preliminary):**
```
Decks:
GET    /api/decks
POST   /api/decks
PUT    /api/decks/{id}
DELETE /api/decks/{id}

Cards:
GET    /api/decks/{deckId}/cards
POST   /api/decks/{deckId}/cards
PUT    /api/cards/{id}
DELETE /api/cards/{id}

Study:
GET    /api/decks/{deckId}/study  (returns due cards)
POST   /api/cards/{id}/review     (submit rating)
```

### SM-2 Algorithm Implementation

**Location:** Hikari.App project - dedicated component for algorithm logic

**Component Design:**
- **Dedicated service/component** (e.g., `ISpacedRepetitionAlgorithm`)
- **Single responsibility** - only calculates next review intervals
- **Consumed by** StudyService, ReviewService, etc. - but logic stays centralized
- **Clean interface** - takes current state + rating, returns new state
- **Pure business logic** - no external dependencies for easy testing

**SM-2 Parameters:**
- Initial ease factor: 2.5
- Minimum ease factor: 1.3
- Rating scale: 1 (Again), 2 (Hard), 3 (Good), 4 (Easy)
- Algorithm follows original SM-2 specification

### Error Handling & Validation

**Validation Strategy:**
- **FluentValidation** on commands and queries at service layer
- **Server-side only** for MVP - all validation rules centralized
- **Domain validation** for business logic (e.g., deck name uniqueness, valid ratings)

**Error Flow:**
- FluentValidation failures ValidationException 400 Bad Request with detailed field errors
- Services throw **domain-specific exceptions** (DeckNotFoundException, InvalidRatingException)
- API layer catches exceptions and maps to **appropriate HTTP status codes**
- **Consistent error response format** with field-level error details for React forms

**Exception Types:**
- ValidationException 400 Bad Request (with validation details)
- NotFoundException 404 Not Found  
- UnauthorizedException 401/403
- DuplicateException 409 Conflict
- etc.

**Benefits:**
- Single source of truth for all validation logic
- No client-server sync complexity
- Secure by default (no bypassing validation)
- Clear error responses for React form handling
