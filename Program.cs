var builder = WebApplication.CreateBuilder(args);

// Controllers for web API
builder.Services.AddControllers();

// Required for Swagger/OpenAPI specification document
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWelcomePage("/");

app.Run();
