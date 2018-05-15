# WebAPIWithGZipDeflate

可參考 Blog 文件(<a href="https://blog.kkbruce.net/2017/10/aspnet-webapi-gzip-deflate-commpression.html" target="_blank">1</a>)(<a href="https://blog.kkbruce.net/2017/10/post-gzip-deflate-data-to-aspnet-webapi.html" target="_blank">2</a>)，之前測試發現 ASP.NET Web API 有個查詢資料量不小(1.92 MB)，並且會有同等級的上傳行為，在 ASP.NET Web API 加入 GZIP/Deflate (解)壓縮來改善效能，資料由 1.92 MB &rarr; 50 KB 壓縮率約 40 倍，這是用一點 CPU 來改善效能的好範例。

1. ASP.NET Web API 實作 GZip, Deflate 解壓縮 Message Handler。
2. 測試專案使用 RestSharp 與 HttpClient 進行大筆 JSON 資料進行 GZip, Deflate 壓縮後 HTTP POST 測試。
3. 測試資料約 4227 KB，資料與 Data Model 均使用 https://www.json-generator.com/ 產生。
