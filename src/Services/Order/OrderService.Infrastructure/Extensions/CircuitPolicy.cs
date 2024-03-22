using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Extensions
{
    public static class CircuitPolicy
    {
        public static AsyncCircuitBreakerPolicy CreatePolicy(
        int exceptionsAllowedBeforeBreaking,
        TimeSpan durationOfBreak)
        {
            // Policy kütüphanesini kullanarak bir Circuit Breaker politikası oluşturuluyor.
            return Policy
                .Handle<NullReferenceException>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking, // Circuit Breaker'ın açılması için izin verilen hata sayısı.
                    durationOfBreak,                 // Circuit Breaker açık kaldığında ne kadar süre geçmesi gerektiği.
                    onBreak: (exception, breakDelay) =>
                    {
                        // Circuit Breaker açıldığında çalışacak kod bloğu.
                        // Loglama: Circuit Breaker açıldığında bir log mesajı yaz.
                        Serilog.Log.Warning($"Circuit breaker opened for {breakDelay.TotalSeconds} seconds due to: {exception.Message}");
                    },
                    onReset: () =>
                    {
                        // Circuit Breaker normale döndüğünde çalışacak kod bloğu.
                        // Loglama: Circuit Breaker normale döndüğünde bir log mesajı yaz.
                        Serilog.Log.Information("Circuit breaker normale döndü.");
                    },
                    onHalfOpen: () =>
                    {
                        // Circuit Breaker yarı açık (half-open) durumuna geçtiğinde çalışacak kod bloğu.
                        // Bu durum, Circuit Breaker'ın yeniden etkinleşmeye hazır olduğu ancak tam olarak kapanmadığı anlamına gelir.
                        // Loglama: Circuit Breaker yarı açık durumuna geçtiğinde bir log mesajı yaz.
                        Serilog.Log.Information("Circuit breaker yarı açık duruma geçti.");
                    }
                );
        }
    }
}
