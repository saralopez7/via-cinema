//Credits: https://www.jqueryscript.net/animation/Confetti-Animation-jQuery-Canvas-Confetti-js.html

(function ($) {
    $.confetti = new function () {
        // globals
        var canvas;
        var ctx;
        var w;
        var h;
        var mp = 150; //max particles
        var particles = [];
        var angle = 0;
        var tiltAngle = 0;
        var confettiActive = true;
        var animationComplete = true;
        var reactivationTimerHandler;
        var animationHandler;

        // objects

        var particleColors = {
            colorOptions: ["DodgerBlue", "OliveDrab", "Gold", "pink", "SlateBlue", "lightblue", "Violet", "PaleGreen", "SteelBlue", "SandyBrown", "Chocolate", "Crimson"],
            colorIndex: 0,
            colorIncrementer: 0,
            colorThreshold: 10,
            getColor: function () {
                if (this.colorIncrementer >= 10) {
                    this.colorIncrementer = 0;
                    this.colorIndex++;
                    if (this.colorIndex >= this.colorOptions.length) {
                        this.colorIndex = 0;
                    }
                }
                this.colorIncrementer++;
                return this.colorOptions[this.colorIndex];
            }
        }

        function ConfettiParticle(color) {
            this.x = Math.random() * w; // x-coordinate
            this.y = (Math.random() * h) - h; //y-coordinate
            this.r = randomFromTo(10, 30); //radius;
            this.d = (Math.random() * mp) + 10; //density;
            this.color = color;
            this.tilt = Math.floor(Math.random() * 10) - 10;
            this.tiltAngleIncremental = (Math.random() * 0.07) + .05;
            this.tiltAngle = 0;

            this.draw = function () {
                ctx.beginPath();
                ctx.lineWidth = this.r / 2;
                ctx.strokeStyle = this.color;
                ctx.moveTo(this.x + this.tilt + (this.r / 4), this.y);
                ctx.lineTo(this.x + this.tilt, this.y + this.tilt + (this.r / 4));
                return ctx.stroke();
            }
        }

        function init() {
            setGlobals();
            InitializeButton();

            $(window).resize(function () {
                w = window.innerWidth;
                h = window.innerHeight;
                canvas.width = w;
                canvas.height = h;
            });
        }

        $(document).ready(initializeConfetti);
        $(window).click(stopConfetti);

        function setGlobals() {
            $("body").append('<canvas id="confettiCanvas" style="position:absolute;top:0;left:0;display:none;z-index:99;"></canvas>');
            canvas = document.getElementById("confettiCanvas");
            ctx = canvas.getContext("2d");
            w = window.innerWidth;
            h = window.innerHeight;
            canvas.width = w;
            canvas.height = h;
        }

        function initializeConfetti() {
            canvas.style.display = "block";
            particles = [];
            animationComplete = false;
            for (var i = 0; i < mp; i++) {
                var particleColor = particleColors.getColor();
                particles.push(new ConfettiParticle(particleColor));
            }
            startConfetti();
        }

        function draw() {
            ctx.clearRect(0, 0, w, h);
            var results = [];
            for (var i = 0; i < mp; i++) {
                (function (j) {
                    results.push(particles[j].draw());
                })(i);
            }
            update();

            return results;
        }

        function randomFromTo(from, to) {
            return Math.floor(Math.random() * (to - from + 1) + from);
        }

        function update() {
            var remainingFlakes = 0;
            var particle;
            angle += 0.01;
            tiltAngle += 0.1;

            for (var i = 0; i < mp; i++) {
                particle = particles[i];
                if (animationComplete) return;

                if (!confettiActive && particle.y < -15) {
                    particle.y = h + 100;
                    continue;
                }

                stepParticle(particle, i);

                if (particle.y <= h) {
                    remainingFlakes++;
                }
                checkForReposition(particle, i);
            }

            if (remainingFlakes === 0) {
                stopConfetti();
            }
        }

        function checkForReposition(particle, index) {
            if ((particle.x > w + 20 || particle.x < -20 || particle.y > h) && confettiActive) {
                if (index % 5 > 0 || index % 2 === 0) //66.67% of the flakes
                {
                    repositionParticle(particle, Math.random() * w, -10, Math.floor(Math.random() * 10) - 10);
                } else {
                    if (Math.sin(angle) > 0) {
                        //Enter from the left
                        repositionParticle(particle, -5, Math.random() * h, Math.floor(Math.random() * 10) - 10);
                    } else {
                        //Enter from the right
                        repositionParticle(particle, w + 5, Math.random() * h, Math.floor(Math.random() * 10) - 10);
                    }
                }
            }
        }
        function stepParticle(particle, particleIndex) {
            particle.tiltAngle += particle.tiltAngleIncremental;
            particle.y += (Math.cos(angle + particle.d) + 3 + particle.r / 2) / 2;
            particle.x += Math.sin(angle);
            particle.tilt = (Math.sin(particle.tiltAngle - (particleIndex / 3))) * 15;
        }

        function repositionParticle(particle, xCoordinate, yCoordinate, tilt) {
            particle.x = xCoordinate;
            particle.y = yCoordinate;
            particle.tilt = tilt;
        }

        function startConfetti() {
            w = window.innerWidth;
            h = window.innerHeight;
            canvas.width = w;
            canvas.height = h;
            (function animloop() {
                if (animationComplete) return null;
                animationHandler = requestAnimFrame(animloop);
                return draw();
            })();
        }

        function clearTimers() {
            clearTimeout(reactivationTimerHandler);
            clearTimeout(animationHandler);
        }

        function deactivateConfetti() {
            confettiActive = false;
            clearTimers();
        }

        function stopConfetti() {
            animationComplete = true;
            if (ctx == undefined) return;
            ctx.clearRect(0, 0, w, h);
            canvas.style.display = 'none';
        }

        function restartConfetti() {
            clearTimers();
            stopConfetti();
            reactivationTimerHandler = setTimeout(function () {
                confettiActive = true;
                animationComplete = false;
                initializeConfetti();
            }, 100);
        }

        window.requestAnimFrame = (function () {
            return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || function (callback) {
                return window.setTimeout(callback, 1000 / 60);
            };
        })();

        this.init = init;
        this.start = initializeConfetti;
        this.stop = deactivateConfetti;
        this.restart = restartConfetti;
    }
    $.confetti.init();
}(jQuery));