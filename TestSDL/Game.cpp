#include "Game.h"
#include <iostream>

namespace TestSDL
{
	Game::Game(std::string title, bool center, int width, int height) :
		_windowTitle(title), _centerWindow(center),
		_windowWidth(width), _windowHeight(height)
	{
		if (SDL_Init(SDL_INIT_VIDEO) < 0)
		{
			throw std::exception("SDL Initialization Error");
		}

		_window = SDL_CreateWindow(_windowTitle.c_str(),
			_centerWindow ? SDL_WINDOWPOS_CENTERED : SDL_WINDOWPOS_UNDEFINED,
			_centerWindow ? SDL_WINDOWPOS_CENTERED : SDL_WINDOWPOS_UNDEFINED,
			_windowWidth, _windowHeight, SDL_WINDOW_SHOWN);

		if (_window == nullptr)
		{
			throw std::exception("SDL Window Creation Error");
		}

		_windowSurface = SDL_GetWindowSurface(_window);

		if (_windowSurface == nullptr)
		{
			throw std::exception("SDL Surface Creation Error");
		}
	}

	Game::~Game()
	{
		if (_windowSurface != nullptr)
		{
			SDL_FreeSurface(_windowSurface);
			_windowSurface = nullptr;
		}

		if (_window != nullptr)
		{
			SDL_DestroyWindow(_window);
			_window = nullptr;
		}

		SDL_Quit();
	}

	void Game::Run()
	{
		Initialize();

		LoadMedia();

		_isRunning = true;
		while (_isRunning)
		{
			_keyboardState.BackupState();

			SDL_Event e;
			while (SDL_PollEvent(&e) != 0)
			{
				if (e.type == SDL_KEYDOWN)
				{
					_keyboardState.UpdateState(e.key.keysym.sym, true);
				}
				else if (e.type == SDL_KEYUP)
				{
					_keyboardState.UpdateState(e.key.keysym.sym, false);
				}

				OnEvent(e);
			}

			// TODO: Add deltaTime parameter
			Update();
			Draw();

			// TODO: Calculate proper wait time
			SDL_Delay(1000 / 60);
		}
	}

	void Game::Exit()
	{
		_isRunning = false;
	}
}
