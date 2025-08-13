import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_scanner/presentation/widgets/doctor_qr_app_bar.dart';
import 'package:heal_link/features/doctor/doctor_scanner/presentation/widgets/doctor_qr_sub_title.dart';
import 'package:heal_link/features/doctor/doctor_scanner/presentation/widgets/doctor_qr_done_button.dart';
import 'package:heal_link/features/doctor/doctor_scanner/presentation/widgets/doctor_qr_generate_qr_button.dart';
import 'package:heal_link/features/doctor/doctor_scanner/presentation/widgets/doctor_qr_image_widget.dart';

class DoctorScannerViewBody extends StatelessWidget {
  const DoctorScannerViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Padding(
        padding: EdgeInsets.symmetric(horizontal: 16),
        child: CustomScrollView(
          slivers: [
            CustomSliverHeight(24),
            DoctorQrAppBar(),
            CustomSliverHeight(32),
            DoctorQrSubTitleWidget(),
            CustomSliverHeight(56),
            DoctorQrImageWidget(),
            CustomSliverHeight(56),
            DoctorQrDoneButton(),
            CustomSliverHeight(24),
            DoctorQrGenerateNewQrButton(),
          ],
        ),
      ),
    );
  }
}
