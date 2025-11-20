const loadBtn = document.getElementById('loadBtn');
const albumContainer = document.getElementById('albumContainer');

loadBtn.addEventListener('click', async () => {
    const albumId = document.getElementById('albumId').value;
    if (!albumId) return alert("Enter album ID");

    try {
        const response = await fetch(`https://localhost:7021/api/albums/${albumId}`);
        
        if (!response.ok) {
            albumContainer.innerHTML = `<p>Album not found</p>`;
            return;
        }

        const album = await response.json();
        renderAlbum(album);
    } catch (err) {
        console.error(err);
        albumContainer.innerHTML = `<p>Error loading album</p>`;
    }
});

function renderAlbum(album) {
    const tracks = album.tracks?.$values || [];

    albumContainer.innerHTML = `
        <div class="album">
            <h2>${album.title} (${album.releaseYear})</h2>
            <p><strong>Artist:</strong> ${album.artist.name} (${album.artist.country})</p>
            <p><strong>Label:</strong> ${album.label.name} (${album.label.country})</p>

            <div class="tracks">
                <h3>Tracks:</h3>
                ${tracks.map(t => `<div class="track">${t.title} (${t.duration}s)</div>`).join('')}
            </div>
        </div>
    `;
}

async function addLabel(){
    const labelId = document.getElementById("labelId").value;
    const name = document.getElementById("labelName").value;
    const country = document.getElementById("labelCountry").value;
    const year = document.getElementById("labelYear").value;
    const msg = document.getElementById("addLabelMsg");

    if (!labelId || !name || !country || !year) {
        msg.textContent = "Please fill all fields!";
        msg.style.color = "red";
        return;
    }
    try {
        const res = await fetch(`https://localhost:7021/api/procedures/addLabel`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ labelId, name, country, foundationYear: year })
        });
        const text = await res.text();
        msg.textContent = text;
    } catch (err) {
        msg.textContent = "Error: " + err;
    }
};

async function changeAlbumLabel() {
    const albumId = document.getElementById("albumIdChange").value;
    const title = document.getElementById("albumTitle").value;
    const artistId = document.getElementById("artistId").value;
    const newLabelId = document.getElementById("newLabelId").value;
    const msg = document.getElementById("changeAlbumMsg");

    if (!albumId || !title || !artistId || !newLabelId) {
        msg.textContent = "Please fill all fields!";
        msg.style.color = "red";
        return;
    }

    try {
        const res = await fetch("https://localhost:7021/api/procedures/changeAlbumLabel", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ albumId, albumTitle: title, artistId, newLabelId })
        });

       const data = await res.json();

        msg.style.color = res.ok;
        msg.textContent = data.message;

    } catch (err) {
        msg.textContent = "Network error: " + err;
        msg.style.color = "red";
    }
}



async function updateAlbumTitle(){
    const albumId = document.getElementById("updateAlbumId").value;
    const suffix = document.getElementById("suffix").value;
    const msg = document.getElementById("updateTitleMsg");

    try {
        const res = await fetch(`https://localhost:7021/api/procedures/updateAlbumTitle`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ albumId, suffix })
        });
         if (!res.ok) {
            const text = await res.text();
            throw new Error(text);
        }

        const text = await res.text();
        msg.textContent = "Updated title: " + text;
    } catch (err) {
        msg.textContent = "Error: " + err;
    }
}

async function loadLessThanAvg() {
    try {
        const res = await fetch("https://localhost:7021/api/functions/lessThanAvg");
        const count = await res.json();
        document.getElementById("lessThanAvgResult").textContent = count;
    } catch (err) {
        document.getElementById("lessThanAvgResult").textContent = "Error: " + err;
    }
}

async function loadLongerThan() {
    const duration = document.getElementById("durationInput").value;
    if (!duration) return alert("Enter a duration!");
    
    try {
        const res = await fetch(`https://localhost:7021/api/functions/longerThan/${duration}`);
        const count = await res.json();
        document.getElementById("longerThanResult").textContent = count;
    } catch (err) {
        document.getElementById("longerThanResult").textContent = "Error: " + err;
    }
}

async function loadTopAlbums() {
    const artist = document.getElementById("artistNameInput").value;
    if (!artist) return alert("Enter artist name!");
    
    try {
        const res = await fetch(`https://localhost:7021/api/functions/topAlbums/${artist}`);
        const data = await res.json();

        const albums = data.$values || data;

        const container = document.getElementById("topAlbumsResult");
        container.innerHTML = albums.map(a => `${a.albumTitle} (${a.trackCount} tracks)`).join("<br>");
    } catch (err) {
        document.getElementById("topAlbumsResult").textContent = "Error: " + err;
    }
}

