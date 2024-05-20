using Microsoft.EntityFrameworkCore;
using ecommerce_temp.Data;
using Microsoft.AspNetCore.Identity;
using ecommerce_temp.Models;
using ecommerce_temp.Service;
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
            // send mail service
            services.AddOptions();
            var mailsetting = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsetting);
            services.AddSingleton<IEmailSender, SendMailService>();

            services.AddRazorPages();
            services.AddDbContext<ecommerce_tempContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ecommerce_tempContext")));

            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ecommerce_tempContext>()
            .AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();
            // Đăng ký CartService
            services.AddScoped<CartService>();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();


            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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
                options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

            });
            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        var googleConfig = Configuration.GetSection("Authentication:Google");
                        options.ClientId = googleConfig["ClientId"];
                        options.ClientSecret = googleConfig["ClientSecret"];
                        options.CallbackPath = "/LoginWithGoogle";
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

            app.UseHttpsRedirection(); //  Chuyển hướng các yêu cầu HTTP sang HTTPS. 
            app.UseStaticFiles(); //  Cho phép phục vụ các tệp tĩnh như CSS, JavaScript, v.v.
            app.UseRouting(); // Thêm middleware định tuyến vào pipeline xử lý yêu cầu.
            app.UseSession();
            app.UseAuthentication(); // Thêm middleware xác thực vào pipeline.
            app.UseAuthorization(); // Thêm middleware phân quyền vào pipeline.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Cart",
                    pattern: "Cart", // Đường dẫn URL mà bạn muốn định tuyến
                    defaults: new { controller = "Cart", action = "Index" }); // Controller và action mặc định
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            }); // Cấu hình các điểm cuối cho ứng dụng. Ở đây, chúng ta định nghĩa một route mặc định cho ứng dụng.
        }
    }
}
