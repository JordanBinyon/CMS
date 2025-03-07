// Clean up initial class after page load and ensure proper state
document.addEventListener('DOMContentLoaded', function() {
    const sidebar = document.querySelector('.sidebar');
    const mainContainer = document.querySelector('.main-container');
    const isCollapsed = localStorage.getItem('sidebarCollapsed') === 'true';

    // Apply the persistent state and remove initial class
    if (isCollapsed && window.innerWidth > 768) {
        sidebar.classList.add('sidebar--collapsed');
        mainContainer.classList.add('main-container--expanded');
    } else {
        sidebar.classList.remove('sidebar--collapsed');
        mainContainer.classList.remove('main-container--expanded');
    }
    document.documentElement.classList.remove('sidebar-initial-collapsed');
});

function toggleSidebar() {
    const sidebar = document.querySelector('.sidebar');
    const mainContainer = document.querySelector('.main-container');
    const isMobile = window.innerWidth <= 768;

    if (!isMobile) {
        sidebar.classList.toggle('sidebar--collapsed');
        mainContainer.classList.toggle('main-container--expanded');
        sidebar.classList.remove('sidebar--mobile-open');

        // Save the state to localStorage
        const isCollapsed = sidebar.classList.contains('sidebar--collapsed');
        localStorage.setItem('sidebarCollapsed', isCollapsed);
    }
}

function toggleMobileSidebar() {
    const sidebar = document.querySelector('.sidebar');
    const isMobile = window.innerWidth <= 768;

    if (isMobile) {
        sidebar.classList.toggle('sidebar--mobile-open');
        sidebar.classList.remove('sidebar--collapsed');
    }
}

function toggleSubmenu(event, type) {
    event.preventDefault();
    const isMobile = window.innerWidth <= 768;
    let menuItem;

    if (type === 'profile') {
        menuItem = document.querySelector('.sidebar__menu-item--profile');
    } else if (type === 'users') {
        menuItem = document.querySelector('.sidebar__menu-item--users');
    }

    if (menuItem && (isMobile || type === 'users') && !document.querySelector('.sidebar').classList.contains('sidebar--collapsed')) {
        menuItem.classList.toggle(type === 'profile' ? 'sidebar__menu-item--profile--active' : 'sidebar__menu-item--users--active');
    }
}