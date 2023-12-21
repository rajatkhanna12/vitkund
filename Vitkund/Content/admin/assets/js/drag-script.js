// drag source element
var dragSrcEl = null;

window.onload = function() {
    setUpEventListeners();
};

/* setUpEventListeners */
function setUpEventListeners() {
    cols = document.querySelectorAll('.col');
    var i=0;
    cols.forEach(function(col) {
        col.style.order = i++; // programatically set order value
        col.addEventListener('dragstart', colDragStart, false);
        col.addEventListener('dragenter', colDragEnter, false);
        col.addEventListener('dragleave', colDragLeave, false);
        col.addEventListener('dragover', colDragOver, false);
        col.addEventListener('drop', colDrop, false);
        col.addEventListener('dragend', colDragEnd, false);

    });
}
/*colDragStart*/
function colDragStart(e) {
    this.style.opacity = '0.4'; // this / e.target is the source node.
    // Set drag source
    dragSrcEl = this;
}

/*colDragEnter*/
function colDragEnter(e) {
    // this / e.target is the current hover target.
    this.classList.add('over');

    var tempOrder = this.style.order;
    this.style.order = dragSrcEl.style.order;
    dragSrcEl.style.order = tempOrder;
}

/* colDragLeave */
function colDragLeave(e) {
    this.classList.remove('over'); // this / e.target is previous target element.
}

/* colDragOver */
function colDragOver(e) {
    if (e.preventDefault) {
        e.preventDefault(); // Necessary. Allows us to drop.
    }
    e.dataTransfer.dropEffect = 'move'; // See the section on the DataTransfer object.
    return false;
}

/* colDrop */
function colDrop(e) {
    // this / e.target is current target element.
    if (e.stopPropagation) {
        e.stopPropagation(); // stops the browser from redirecting.
    }
    return false;
}

/*colDragEnd*/
function colDragEnd(e) {
    this.style.opacity = '1'; // this / e.target is the source node.
    cols = document.querySelectorAll('.col');
    cols.forEach(function(col) {
        col.classList.remove('over');
    });
}