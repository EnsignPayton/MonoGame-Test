#include "KeyboardState.h"

namespace TestSDL
{
	KeyboardState::KeyboardState()
	{
	}


	KeyboardState::~KeyboardState()
	{
	}

	void KeyboardState::BackupState()
	{
		_pressedKeys.swap(_pressedKeysPrev);
	}

	void KeyboardState::UpdateState(SDL_Keycode keycode, bool isDown)
	{
		_pressedKeys[keycode] = isDown;
	}
}
