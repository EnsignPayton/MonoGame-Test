#pragma once
#include <SDL.h>
#include <map>

namespace TestSDL
{
	struct KeyboardState
	{
		KeyboardState();
		~KeyboardState();

		std::map<SDL_Keycode, bool>& GetPressedKeys() { return _pressedKeys; };
		bool KeyDown(SDL_Keycode key) { return _pressedKeys[key]; };
		bool KeyUp(SDL_Keycode key) { return _pressedKeys[key]; };
		bool KeyPressed(SDL_Keycode key) { return _pressedKeys[key] && !_pressedKeysPrev[key]; }

		void BackupState();
		void UpdateState(SDL_Keycode keycode, bool isDown);

	private:
		std::map<SDL_Keycode, bool> _pressedKeys;
		std::map<SDL_Keycode, bool> _pressedKeysPrev;
	};
}

