// Scripts for ColourTherapy application

// Create floating petals animation
window.createFloatingPetals = () => {
    const colors = ['#F9D949', '#F88379', '#ACCBE1', '#C8A2C8', '#90EE90'];
    const container = document.body;

    // Create 20 petals
    for (let i = 0; i < 20; i++) {
        setTimeout(() => {
            const petal = document.createElement('div');
            petal.className = 'floating-petal';
            
            // Random position
            const posX = Math.random() * 100;
            const startY = Math.random() * 20 + 100;
            
            // Random size
            const size = Math.random() * 20 + 15;
            
            // Random color
            const color = colors[Math.floor(Math.random() * colors.length)];
            
            // Random animation duration
            const duration = Math.random() * 15 + 10;
            const delay = Math.random() * 5;
            
            // Apply styles
            petal.style.left = `${posX}%`;
            petal.style.top = `${startY}px`;
            petal.style.width = `${size}px`;
            petal.style.height = `${size}px`;
            petal.style.backgroundColor = color;
            petal.style.animationDuration = `${duration}s`;
            petal.style.animationDelay = `${delay}s`;
            
            // Add to DOM
            container.appendChild(petal);
            
            // Remove after animation
            setTimeout(() => {
                petal.remove();
            }, (duration + delay) * 1000);
        }, i * 300);
    }
};

// Function to generate a simple PDF from the results
window.generatePDF = (resultData) => {
    alert('This would generate a PDF with your color therapy results.');
    // In a production app, this would use a library like jsPDF to create a downloadable PDF
};

// Initialize animations when page loads
document.addEventListener('DOMContentLoaded', () => {
    window.createFloatingPetals();
    
    // Setup interval to continue creating petals
    setInterval(() => {
        window.createFloatingPetals();
    }, 30000); // Every 30 seconds
});
