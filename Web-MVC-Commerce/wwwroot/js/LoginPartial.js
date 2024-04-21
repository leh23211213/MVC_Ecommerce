document.addEventListener("DOMContentLoaded", function () {
    const profile = document.querySelector('.profile');
    const dropdown = document.querySelector('.dropdown__wrapper');
    let isDropdownVisible = false; // Biến lưu trữ trạng thái của dropdown

    profile.addEventListener('click', () => {
        isDropdownVisible = !isDropdownVisible; // Khi click vào profile, đảo ngược trạng thái của dropdown
        if (isDropdownVisible) {
            dropdown.classList.remove('none');
        }
        dropdown.classList.toggle('hide');
        dropdown.classList.toggle('dropdown__wrapper--fade-in');
    })

    document.addEventListener("click", (event) => {
        const isClickInsideDropdown = dropdown.contains(event.target);
        const isProfileClicked = profile.contains(event.target);

        if (!isClickInsideDropdown && !isProfileClicked) {
            isDropdownVisible = false; // Nếu click bên ngoài dropdown và profile, đặt trạng thái của dropdown là false
            dropdown.classList.add('hide');
            dropdown.classList.remove('dropdown__wrapper--fade-in');
        } else {
            isDropdownVisible = true; // Nếu click vào dropdown hoặc profile, đặt trạng thái của dropdown là true
            dropdown.classList.remove('hide');
            dropdown.classList.add('dropdown__wrapper--fade-in');
        }
    });
});
