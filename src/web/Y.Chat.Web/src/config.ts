let url:string;

if (import.meta.env.MODE === "development") {
    url = "http://localhost:5088"
} else {
    url = '';
}

const config = {
    API: url,
    UploadAvatarApi:url+'/api/v1/Files/UploadAvatar',
    getFile:(filename:string)=>url+'/api/v1/Files/File?filename='+filename,
    defaultAvatar:'./systemImages/default_avatar.jpg'
}

export default config