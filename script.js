// Slider functionality
document.addEventListener('DOMContentLoaded', function() {
    const slides = document.querySelectorAll('.slide');
    const dots = document.querySelectorAll('.dot');
    const prevButton = document.querySelector('.prev-slide');
    const nextButton = document.querySelector('.next-slide');
    let currentSlide = 0;
    let slideInterval;

    // Debug için butonları kontrol et
    console.log('Prev Button:', prevButton);
    console.log('Next Button:', nextButton);

    function showSlide(index) {
        // Remove active class from all slides and dots
        slides.forEach(slide => {
            slide.classList.remove('active', 'prev', 'next');
        });
        dots.forEach(dot => dot.classList.remove('active'));

        // Add active class to current slide and dot
        slides[index].classList.add('active');
        dots[index].classList.add('active');

        // Add prev and next classes for animation
        const prevIndex = (index - 1 + slides.length) % slides.length;
        const nextIndex = (index + 1) % slides.length;
        slides[prevIndex].classList.add('prev');
        slides[nextIndex].classList.add('next');
    }

    function nextSlide() {
        currentSlide = (currentSlide + 1) % slides.length;
        showSlide(currentSlide);
    }

    function prevSlide() {
        currentSlide = (currentSlide - 1 + slides.length) % slides.length;
        showSlide(currentSlide);
    }

    // Event listeners
    if (nextButton) {
        nextButton.addEventListener('click', function(e) {
            e.preventDefault();
            console.log('Next button clicked');
            clearInterval(slideInterval);
            nextSlide();
            startSlideInterval();
        });
    }

    if (prevButton) {
        prevButton.addEventListener('click', function(e) {
            e.preventDefault();
            console.log('Prev button clicked');
            clearInterval(slideInterval);
            prevSlide();
            startSlideInterval();
        });
    }

    dots.forEach((dot, index) => {
        dot.addEventListener('click', () => {
            clearInterval(slideInterval);
            currentSlide = index;
            showSlide(currentSlide);
            startSlideInterval();
        });
    });

    function startSlideInterval() {
        slideInterval = setInterval(nextSlide, 5000);
    }

    // Start the slider
    showSlide(currentSlide);
    startSlideInterval();
}); 