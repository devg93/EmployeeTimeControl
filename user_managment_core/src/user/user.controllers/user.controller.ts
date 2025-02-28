import { Controller, Get, Post, Body, Patch, Param, Delete, Inject } from '@nestjs/common';

import { CreateRegistracionDto } from '../dto/create-registracion.dto';
import { UpdateRegistracionDto } from '../dto/update-registracion.dto';
import { ApiOperation } from '@nestjs/swagger';
import { UserInterface } from '../user.repository/contracts/user.repository.Interface';



@Controller('registracion')
export class RegistracionController {
  constructor(@Inject("UserInterface")private readonly UserService: UserInterface) {}

  @Post('register')
  @ApiOperation({ summary: 'Register a new user' })
  async register(@Body() body: CreateRegistracionDto) {
    
    return   await this.UserService.register(body);

  }


  @Get('findAll')
  async findAll() {  

    return await this.UserService.findAll();
  }


  @Get('findOne/:id')
  async findOne(@Param('id') id: string) {
    return await this.UserService.getProfileById(id);
  }


  @Patch('update/:id')
  async update(@Param('id') id: string, @Body() updateRegistracionDto: UpdateRegistracionDto) {
    return await this.UserService.updateUser(id, updateRegistracionDto);
  }
  

  @Delete('delete/:id')
  async remove(@Param('id') id: string) {
     return await this.UserService.remove(id);
  }
  
}
