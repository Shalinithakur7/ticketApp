var builder = WebApplication.CreateBuilder(args);

// --- 1. Add Services ---
// CORS Policy for Angular App (http://localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// --- 2. Configure Pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Apply the CORS Policy here
app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();
app.Run();