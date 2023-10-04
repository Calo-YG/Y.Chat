var url;

if (import.meta.env.MODE === "development") {
    url = "http://localhost:5088"
} else {
    url = '';
}

const config = {
    API: url
}

export default config