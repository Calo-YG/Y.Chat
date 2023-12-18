let url:string;

if (import.meta.env.MODE === "development") {
    url = "http://localhost:5173"
} else {
    url = 'http://120.26.216.57:5173';
}

const config = {
    API: url,
    UploadAvatarApi:url+'/api/v1/Files/UploadAvatar',
    getFile:(filename:string)=>url+'/api/v1/Files/File?filename='+filename,
    defaultAvatar:'./systemImages/default_avatar.jpg'
}

export default config