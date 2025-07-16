using PolicyProcessor.Contracts.Models;

// ReSharper disable once CheckNamespace
namespace PolicyProcessor.Contracts.Api;

public record GetInsurancePolicyResponse(
    InsurancePolicy InsurancePolicy);