window.blazorHelpers = {
    scrollToFragment: (elementId) => {
        let element = document.getElementById(elementId);
        if (element) {
            element.scrollIntoView({
                behavior: 'smooth'
            });
        }
    }
};