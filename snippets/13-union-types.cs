public record Success<T>(T Value);
public record Failure(string Error);

public union Result<T>(Success<T>, Failure);
