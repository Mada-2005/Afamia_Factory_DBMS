/* ============================================
   AFAMIA UI - Animation & Interaction System
   ============================================
   Advanced JavaScript for creating stunning
   interactive experiences
   ============================================ */

// Wait for DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {

    // Initialize all animations
    initAnimations();
    initCardAnimations();
    initButtonRipples();
    initTableAnimations();
    initFormAnimations();
    initScrollAnimations();

});

/* ------------------------------------
   1. INITIALIZATION ANIMATIONS
   ------------------------------------ */
function initAnimations() {
    // Fade in elements with stagger effect
    const fadeElements = document.querySelectorAll('[data-animate="fade"]');
    fadeElements.forEach((el, index) => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(20px)';
        setTimeout(() => {
            el.style.transition = 'all 0.6s ease';
            el.style.opacity = '1';
            el.style.transform = 'translateY(0)';
        }, index * 100);
    });
}

/* ------------------------------------
   2. CARD HOVER ANIMATIONS
   ------------------------------------ */
function initCardAnimations() {
    const cards = document.querySelectorAll('.card-modern, .card-dashboard, .card-animated');

    cards.forEach(card => {
        // Add 3D tilt effect on mouse move
        card.addEventListener('mousemove', (e) => {
            const rect = card.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            const centerX = rect.width / 2;
            const centerY = rect.height / 2;

            const rotateX = (y - centerY) / 20;
            const rotateY = (centerX - x) / 20;

            card.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) translateY(-5px)`;
        });

        card.addEventListener('mouseleave', () => {
            card.style.transform = 'perspective(1000px) rotateX(0) rotateY(0) translateY(0)';
        });
    });
}

/* ------------------------------------
   3. BUTTON RIPPLE EFFECT
   ------------------------------------ */
function initButtonRipples() {
    const buttons = document.querySelectorAll('.btn-modern, .btn-animated, .btn');

    buttons.forEach(button => {
        button.addEventListener('click', function(e) {
            const ripple = document.createElement('span');
            const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);
            const x = e.clientX - rect.left - size / 2;
            const y = e.clientY - rect.top - size / 2;

            ripple.style.width = ripple.style.height = size + 'px';
            ripple.style.left = x + 'px';
            ripple.style.top = y + 'px';
            ripple.classList.add('ripple');

            this.appendChild(ripple);

            setTimeout(() => ripple.remove(), 600);
        });
    });
}

/* ------------------------------------
   4. TABLE ROW ANIMATIONS
   ------------------------------------ */
function initTableAnimations() {
    const tableRows = document.querySelectorAll('.table-modern tbody tr, .table-animated tbody tr');

    // Stagger animation on page load
    tableRows.forEach((row, index) => {
        row.style.opacity = '0';
        row.style.transform = 'translateX(-20px)';
        setTimeout(() => {
            row.style.transition = 'all 0.4s ease';
            row.style.opacity = '1';
            row.style.transform = 'translateX(0)';
        }, index * 50);
    });

    // Add click animation
    tableRows.forEach(row => {
        row.addEventListener('click', function(e) {
            // Don't animate if clicking a button
            if (e.target.tagName === 'BUTTON' || e.target.tagName === 'A') return;

            this.style.transform = 'scale(0.98)';
            setTimeout(() => {
                this.style.transform = 'scale(1)';
            }, 200);
        });
    });
}

/* ------------------------------------
   5. FORM INPUT ANIMATIONS
   ------------------------------------ */
function initFormAnimations() {
    const inputs = document.querySelectorAll('.input-modern, .form-control');

    inputs.forEach(input => {
        // Add floating label effect
        input.addEventListener('focus', function() {
            this.parentElement.classList.add('focused');
        });

        input.addEventListener('blur', function() {
            if (!this.value) {
                this.parentElement.classList.remove('focused');
            }
        });

        // Add validation pulse animation
        input.addEventListener('invalid', function() {
            this.classList.add('shake-animation');
            setTimeout(() => {
                this.classList.remove('shake-animation');
            }, 500);
        });
    });
}

/* ------------------------------------
   6. SCROLL ANIMATIONS
   ------------------------------------ */
function initScrollAnimations() {
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);

    // Observe elements with scroll-animation class
    const scrollElements = document.querySelectorAll('[data-scroll-animation]');
    scrollElements.forEach(el => observer.observe(el));
}

/* ------------------------------------
   7. UTILITY FUNCTIONS
   ------------------------------------ */

// Add shake animation to element
function shakeElement(element) {
    element.classList.add('shake-animation');
    setTimeout(() => element.classList.remove('shake-animation'), 500);
}

// Add pulse animation to element
function pulseElement(element) {
    element.classList.add('pulse-animation');
    setTimeout(() => element.classList.remove('pulse-animation'), 1000);
}

// Show loading state on button
function setButtonLoading(button, isLoading) {
    if (isLoading) {
        button.classList.add('btn-loading');
        button.disabled = true;
    } else {
        button.classList.remove('btn-loading');
        button.disabled = false;
    }
}

// Smooth scroll to element
function smoothScrollTo(elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
}

/* ------------------------------------
   8. ADDITIONAL CSS FOR RIPPLE EFFECT
   ------------------------------------ */
// Add ripple styles dynamically
const rippleStyles = `
    <style>
        .ripple {
            position: absolute;
            border-radius: 50%;
            background: rgba(255, 255, 255, 0.6);
            transform: scale(0);
            animation: ripple-animation 0.6s ease-out;
            pointer-events: none;
        }

        @keyframes ripple-animation {
            to {
                transform: scale(2);
                opacity: 0;
            }
        }

        .shake-animation {
            animation: shake 0.5s ease-in-out;
        }

        @keyframes shake {
            0%, 100% { transform: translateX(0); }
            10%, 30%, 50%, 70%, 90% { transform: translateX(-5px); }
            20%, 40%, 60%, 80% { transform: translateX(5px); }
        }

        .pulse-animation {
            animation: pulse 1s ease-in-out;
        }

        @keyframes pulse {
            0%, 100% { transform: scale(1); }
            50% { transform: scale(1.05); }
        }

        .animate-in {
            animation: fadeInUp 0.8s ease-out forwards;
        }

        .btn {
            position: relative;
            overflow: hidden;
        }
    </style>
`;

// Inject ripple styles
document.head.insertAdjacentHTML('beforeend', rippleStyles);

/* ------------------------------------
   9. FORM VALIDATION ENHANCEMENTS
   ------------------------------------ */
// Enhanced form validation with visual feedback
document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', function(e) {
        const submitBtn = this.querySelector('button[type="submit"]');
        if (submitBtn && !submitBtn.classList.contains('btn-loading')) {
            // Add loading state only if form is valid
            const isValid = this.checkValidity();
            if (isValid) {
                setButtonLoading(submitBtn, true);
            }
        }
    });
});

/* ------------------------------------
   10. DELETE CONFIRMATION ENHANCEMENT
   ------------------------------------ */
// Enhance delete buttons with modern confirmation
document.querySelectorAll('form[asp-page-handler="Delete"], button[onclick*="confirm"]').forEach(element => {
    const form = element.tagName === 'FORM' ? element : element.closest('form');
    if (form) {
        form.addEventListener('submit', function(e) {
            const deleteBtn = this.querySelector('button[type="submit"]');
            if (deleteBtn) {
                deleteBtn.style.transition = 'all 0.3s ease';
            }
        });
    }
});
