<html><head>
                        <meta charset='utf-8' />
                        <title></title>
                        <style>
                        @mixin media() {
                        @media (min-width: 768px) {
                        @content;
                        }
                        }
 
                        body, html {
                        font-family: 'Vollkorn', serif;
                        font-weight: 400;
                        line-height: 1.3;
                        font-size: 16px;
                        }
 
                        .siteTitle {
                        display: block;
                        font-weight: 900;
                        font-size: 30px;
                        margin: 20px 0;
	
                        @include media {
                        font-size: 60px;
                        }
                        }
 
                        header,
                        main,
                        footer {
                        max-width: 960px;
                        margin: 0 auto;
                        }
 
                        .card {
                        height: 400px;
                        position: relative;
                        padding: 20px;
                        box-sizing: border-box;
                        display: flex;
                        align-items: flex-end;
                        text-decoration: none;
                        border: 4px solid #b0215e;
                        margin-bottom: 20px;
                        //background-image: url('https://baylorlariat.com/wp-content/uploads/2018/02/Iron-Man-Movie_Poster_2008.jpg');
                        background-size: cover;
	
                        @include media {
                        height: 500px;
                        }
                        }
 
                        .inner {
                        height: 50%;
                        display: flex;
                        flex-direction: column;
                        justify-content: center;
                        align-items: center; 
                        background: white;
                        box-sizing: border-box;
                        padding: 40px;
	
                        @include media {
                        width: 50%;
                        height: 100%;
                        }
                        }
 
                        .title {
                        font-size: 24px;
                        color: black;  
                        text-align: center;
                        font-weight: 700;
                        color: #181818;
                        text-shadow: 0px 2px 2px #a6f8d5;
                        position: relative;
                        margin: 0 0 20px 0;
	
                        @include media {
                        font-size: 30px;
                        }
                        }
                        </style>
                        </head>
                        <body>
                        <div  class='card'>
                        <div class='inner'>
                        <h1><b>Reminder</b></h1>

                        <span>
                            <h4>HI <span style='font-size: 18px;'>{{name}}</span>, </h4>
                            <h2></h2>
                            <h4>This is the reminder for you that your course <span style='font-size: 18px;'>{{course}}</span>, </h4>
                            <!-- <h2>{{course}}</h2> -->
                            <h4>is on <span style='font-size: 18px;'>{{startdate}}</span></h4>
                            <h4>Following is the list to prepare your visit at <span style='font-size: 18px;'>{{enddate}}</span></h4>
                            <h4>Your registration status is {{status}} <span style='font-size: 18px;'>{{status}}</span></h4>
                            <!-- <h2>{{startdate}}</h2> -->
                            <!--<time class='subtitle'>Reminder<time>-->
                        </span>

                        </div>
                        </div>
                        </body>
                        </html>