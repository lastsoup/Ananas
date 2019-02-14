using Ananas.Web.Mvc.Models;
using Ananas.Web.Mvc.Extensions;
using Ananas.Web.Mvc.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using System.Threading.Tasks;

namespace Ananas.Web.Mvc.Examples.Controllers
{
    [Route("api/WebAPI")]
    [ApiController]
    public class WebAPIController: ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public WebAPIController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        static List<UserInfo> users = new List<UserInfo>();
        [HttpPost("GetPostParams")]
        public PageInfo GetPostParams()
        {
            if (users.Count == 0)
                InitUsers();
            int count = int.Parse(Request.Form["length"]);//repage.length;
            int start = int.Parse(Request.Form["start"]);//repage.start;
            List<UserInfo> reUsers = new List<UserInfo>();
            int i = 0;
            while (i != count)
            {
                int index = start + i;
                if (index > users.Count - 1) break;
                reUsers.Add(users[start + i]);
                i++;
            }
            var page = new PageInfo();
            page.data = reUsers;
            page.recordsTotal = users.Count;
            page.recordsFiltered = users.Count;
            return page;
        }

        [HttpPost("GetUserList")]
        public PageInfo GetUserList([FromBody] PageInfo repage)
        {
            //初始化
            if (users.Count == 0)
                InitUsers();
            int count = repage.length;
            int start = repage.start;
            List<UserInfo> reUsers = new List<UserInfo>();
            int i = 0;
            while (i != count)
            {
                int index = start + i;
                if (index > users.Count - 1) break;
                reUsers.Add(users[start + i]);
                i++;
            }
            var page = new PageInfo();
            page.data = reUsers;
            page.recordsTotal = users.Count;
            page.recordsFiltered = users.Count;
            return page;
        }

         //初始化假数据
        void InitUsers()
        {
            for (int i = 0; i < 100; i++)
            {
                var number=i+1;
                var user = new UserInfo { ID = number.ToString()};
                user.Name = $@"{i}-{user.ID}";
                users.Add(user);
            }
        }

        

        [HttpPost("GetJWT")]
        public OkObjectResult GetJWT([FromBody] UserInfo user)
        {
            if (user.Name == "admin" && user.Password == "12345")
            {
                JwtTokenUtil jwtTokenUtil = new JwtTokenUtil(_configuration);
                string token = jwtTokenUtil.GetToken(user);   //生成token
                return Ok(new JsonBase() { IsSuccess = true,Message="登录成功",Token=token});
            }
            else
            {
                 return Ok(new JsonBase() { IsSuccess = false,Message="登陆失败" });
            }
        }


         char namechar='@'; 
         [HttpPost("GetFileList")]
         public IActionResult GetFileList()
        {
             try
            {
                //string contentRootPath = _hostingEnvironment.ContentRootPath;
                var filesdir=Path.Combine(_hostingEnvironment.WebRootPath,"Upload");
                DirectoryInfo root = new DirectoryInfo(filesdir);
                List<Object> lstFile = new List<Object>();
                foreach (FileInfo f in root.GetFiles())
                {
                    var name=f.Name.Split(namechar);
                    var path="/Upload/"+f.Name;
                    var size=f.Length.ToString();
                    var extension=f.Extension;
                    var memi ="";
                    if(extension!=".bak"){

                        var provider = new FileExtensionContentTypeProvider();
                        memi = provider.Mappings[extension];
                    }
                    lstFile.Add(new { AttachID = name[0],AttachName=name[1],AttachPath=path,AttachSize=size,AttachType=memi});
                }   
                return Ok(new JsonBase() { IsSuccess = true,Message="文件获取成功",BaseData=lstFile });
            }
            catch (Exception ex){
                return Ok(new { IsSuccess = false,Message=ex.Message }); 
            }
        }

        [HttpPost("DeleteFile")]
         public IActionResult DeleteFile()
        {
              try
            {
                var file_Path = Request.Form["file_Path"].ToString();
                var file_ID = Request.Form["file_ID"].ToString();
                var filepath=_hostingEnvironment.WebRootPath+file_Path;
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath); 
                    return Ok(new JsonBase() { IsSuccess = true,Message="删除成功" });
                }else
                {
                     return Ok(new { IsSuccess = false,Message="文件不存在" }); 
                }
               
             }
            catch (Exception ex){
                return Ok(new { IsSuccess = false,Message=ex.Message }); 
            }
        }


        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromServices]IHostingEnvironment environment)
        {
            
            string filepath = string.Empty;
            string filename = string.Empty;
            string fileid = string.Empty;
            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0) { 
                return Ok(new { upload_State = false,msg="请选择上传的文件" }); 
            }
            
            try
            {
                
                foreach (var file in files)
                {
                    filename=file.FileName;
                    if(filename.Contains(namechar)){
                        return Ok(new { upload_State = false,msg="文件名不能包含"+namechar+"请修改后上传" });
                    }
                    string strdir= Path.Combine(environment.WebRootPath,"Upload");
                    if (!Directory.Exists(strdir))
                    {
                        Directory.CreateDirectory(strdir);
                    }
                    fileid=DateTime.Now.ToString("yyyyMMddHHmmss");
                    filepath = Path.Combine(strdir,fileid +namechar+filename);
                    using (var stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                }
               var serverpath="/Upload/"+fileid +namechar+filename;
               return Ok(new { upload_State = true,msg="上传成功",file_ID=fileid,file_Name= filename,file_Path=serverpath});
            }
            catch (Exception ex){
                return Ok(new { upload_State = false,msg=ex.Message }); 
            }
        }
    }
}