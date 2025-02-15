import { Controller, Post, Body, Request, UseGuards, UnauthorizedException } from '@nestjs/common';

import { ApiTags, ApiOperation, ApiBearerAuth } from '@nestjs/swagger';

import { CreateRegistracionDto } from 'src/user/dto/create-registracion.dto';


import { getProfileDto } from 'src/user/dto/getProfileDto';
import { AuthService } from '../user.services/auth.service';
import { CreateAuthDto } from '../dto/create-auth.dto';
import { JwtAuthGuard } from '../libs/jwt-auth.guard';
import { GetUser } from '../libs/decorators/getUser';


@ApiTags('Auth')
@Controller('auth')
export class AuthController {
  constructor(private readonly authService: AuthService) { }

  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {
    return this.authService.register(body);
  }



  @Post('login')
  @ApiOperation({ summary: 'Login and receive a JWT token' })
  async login(@Body() body: CreateAuthDto) {

    const user = await this.authService.validateUser(body.email, body.password);
    if (!user) {
      throw new UnauthorizedException('Invalid credentials');
    }
    return this.authService.login(user);
  }



  @Post('profile')
  @UseGuards(JwtAuthGuard)
  async getProfile(@Body() body: any, @GetUser() user: any): Promise<any> {

    if (!user) {
      throw new UnauthorizedException('User not found');
    }
    const res = await this.authService.getProfileById(user.email);

    return res;

  }

}






