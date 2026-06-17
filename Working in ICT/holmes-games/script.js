// Mobile Menu Toggle
function toggleMobileMenu() {
    const navLinks = document.querySelector('.nav-links');
    navLinks.classList.toggle('active');
}

// Close mobile menu when clicking outside
document.addEventListener('click', function(event) {
    const navLinks = document.querySelector('.nav-links');
    const mobileBtn = document.querySelector('.mobile-menu-btn');
    
    if (navLinks && navLinks.classList.contains('active')) {
        if (!event.target.closest('.nav-links') && !event.target.closest('.mobile-menu-btn')) {
            navLinks.classList.remove('active');
        }
    }
});

// Simple form validation display (intentionally weak for training purposes)
function validateContactForm(event) {
    const form = event.target;
    const name = form.querySelector('#name').value;
    const email = form.querySelector('#email').value;
    const password = form.querySelector('#password');
    const message = form.querySelector('#message').value;
    
    // Basic check - intentionally minimal
    if (!name || !email || !message) {
        alert('Please fill in all required fields');
        event.preventDefault();
        return false;
    }
    
    // INSECURE: Display submitted data including password in alert (BAD PRACTICE)
    if (password) {
        alert('Form submitted!\n\nName: ' + name + '\nEmail: ' + email + '\nPassword: ' + password.value + '\nMessage: ' + message);
    } else {
        alert('Form submitted!\n\nName: ' + name + '\nEmail: ' + email + '\nMessage: ' + message);
    }
    
    event.preventDefault();
    return false;
}

// Attach form validation if contact form exists
document.addEventListener('DOMContentLoaded', function() {
    const contactForm = document.querySelector('.contact-form');
    if (contactForm) {
        contactForm.addEventListener('submit', validateContactForm);
    }
});

// Smooth scroll for anchor links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function(e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth'
            });
        }
    });
});
