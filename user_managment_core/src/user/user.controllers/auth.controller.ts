

import { ApiTags, ApiOperation } from '@nestjs/swagger';
import { CreateAuthDto } from '../dto/create-auth.dto';
import { JwtAuthGuard } from '../libs/jwt-auth.guard';
import { GetUser } from '../libs/decorators/getUser';
import { IuserReadService } from '../libs/contracts/user.repository.Interface';
import { Body, Controller, Get, Inject, Injectable, Param, Post, Scope, UnauthorizedException, UseGuards } from '@nestjs/common';

@Injectable({ scope: Scope.DEFAULT }) 
@ApiTags('Auth')
@Controller('auth')
export class AuthController {
 constructor(@Inject("IuserReadService") private readonly UseReadService: IuserReadService) { }


  @Post('login')
  @ApiOperation({ summary: 'Login and receive a JWT token' })
  async login(@Body() body: CreateAuthDto) {

    const user = await this.UseReadService.loginService(body);
    return user;
 
  }



  @Post('getProfileByemail')
  @UseGuards(JwtAuthGuard)
  async getProfileByemail(@Body() body: any, @GetUser() user: any): Promise<any> {
    if (!user) {
      throw new UnauthorizedException('User not found');
    }
    console.log('getProfileByemail',body)
    return await this.UseReadService.getProfileByEmailService(user.email);

  }

    @Get('findAll')
    async findAll(body:any) {
  
      return await this.UseReadService.findAllService(body);
    }
  
  
    @Get('findOne/:id')
    async findOne(@Param('id') id: string) {
      return await this.UseReadService.getProfileByIdService(id);
    }




}




