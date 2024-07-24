using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Data;
using Microsoft.AspNetCore.Identity;
using ecommerce_temp.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using ecommerce_temp.Data.Models;
using ecommerce_temp.Controllers;


namespace ecommerce_temp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Account/Login";
           }).AddGoogle(options =>
                    {
                        var googleConfig = Configuration.GetSection("Authentication:Google");
                        options.ClientId = googleConfig["ClientId"];
                        options.ClientSecret = googleConfig["ClientSecret"];
                        options.CallbackPath = "/LoginWithGoogle";
                    });

            services.AddAuthorization();

            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ecommerce_tempContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 6; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 0; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lần thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

            });
            // Database context
            services.AddDbContext<ecommerce_tempContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            
            // Session configuration
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Email sender service
            services.AddOptions();
            var mailsetting = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsetting);
            services.AddSingleton<IEmailSender, SendMailService>();

            // Register CartService and UserManager<User> correctly
            services.AddScoped<CartService>();
            services.AddHttpContextAccessor();
            // UserManager is registered by AddIdentity, no need to register it manually
            services.AddHttpContextAccessor();


            // Configure cookie settings if needed
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
        options.OnAppendCookie = cookieContext =>
            CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
        options.OnDeleteCookie = cookieContext =>
            CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
    });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Hiển thị trang lỗi chi tiết khi ứng dụng đang trong môi trường phát triển.
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); // Thêm HTTP Strict Transport Security (HSTS) headers.
            }

            app.UseSession();
            app.UseHttpsRedirection(); //  Chuyển hướng các yêu cầu HTTP sang HTTPS. 
            app.UseStaticFiles(); //  Cho phép phục vụ các tệp tĩnh như CSS, JavaScript, v.v.
            app.UseRouting(); // Thêm middleware định tuyến vào pipeline xử lý yêu cầu.
            app.UseAuthentication(); // Thêm middleware xác thực vào pipeline.
            app.UseAuthorization(); // Thêm middleware phân quyền vào pipeline.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                if (DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = SameSiteMode.Unspecified;
                }
            }
        }

        private bool DisallowsSameSiteNone(string userAgent)
        {
            // Check if the UserAgent is known to incorrectly handle SameSite=None
            if (string.IsNullOrWhiteSpace(userAgent))
            {
                return false;
            }

            // Chrome 51-66
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            // TODO: Add checks for other browsers
            return false;
        }
    }

    // TODO : lập các hàm service để ngắng gọn lại file startup.cs
}
