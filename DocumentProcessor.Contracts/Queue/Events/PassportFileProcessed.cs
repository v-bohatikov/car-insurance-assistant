using DocumentProcessor.Contracts.Models;

namespace DocumentProcessor.Contracts.Queue.Events;

public record PassportFileProcessed(
    Ulid UserId,
    Ulid DocumentId,
    PassportData PassportData);