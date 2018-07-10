#pragma once
#include <SDL.h>
#include <string>
#include "KeyboardState.h"

namespace TestSDL
{
	class Game
	{
	public:
		Game(std::string title = "SDL Window",
			bool center = false,
			int width = 800,
			int height = 600);
		~Game();

		void Run();

	protected:
		virtual void Initialize() = 0 {};
		virtual void LoadMedia() = 0 {};
		virtual void Update() = 0 {};
		virtual void Draw() = 0 {};
		virtual void OnEvent(SDL_Event e) = 0 {};

		void Exit();

		int GetWindowWidth() { return _windowWidth; };
		int GetWindowHeight() { return _windowHeight; };

		SDL_Window* GetWindow() { return _window; };
		SDL_Surface* GetWindowSurface() { return _windowSurface; };
		KeyboardState& GetKeyboardState() { return _keyboardState; };

	private:
		std::string _windowTitle;
		bool _centerWindow;
		int _windowWidth;
		int _windowHeight;
		bool _isRunning;
		SDL_Window* _window;
		SDL_Surface* _windowSurface;
		KeyboardState _keyboardState;
	};
}

