const PROXY_CONFIG = [
    {
        context: [
            "/api"
        ],
        target: "http://localhost:40443",
        secure: false
    }
]

module.exports = PROXY_CONFIG;
